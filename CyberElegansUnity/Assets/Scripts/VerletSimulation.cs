using UnityEngine;

namespace Orbitaldrop.Cyberelegans.Verlet
{
	public struct MassPoint
	{
		public Vector3 pos;
		public bool anchored;

		public Vector3 accel;
		public Vector3 vel;

		public Vector3 previousPosition;

		public MassPoint(float m, Vector3 p) : this(m, p.x, p.y, p.z)
		{
			// Does nothing.
		}
		
		public MassPoint(float m, float x, float y, float z)
		{
			pos = new Vector3(x, y, z);
			anchored = false;
			accel = Vector3.zero;
			vel = Vector3.zero;
			previousPosition = pos;
		}
	}

	public struct Spring
	{
		public int PointIndex1;

		public int PointIndex2;

		public float Strength;

		public float RestLength;

		public Spring(MassPoint[] massPoint, int index1, int index2, float strength)
		{
			PointIndex1 = index1;
			PointIndex2 = index2;
			Strength = strength;
			RestLength = (massPoint[PointIndex2].pos - massPoint[PointIndex1].pos).magnitude;
		}
	}

	public interface IVerletShapeFactory
	{
		void Createshape(Vector3 origin, out MassPoint[] massPoints, out Spring[] springs);
	}

	public class BasicCubeVerletFactory : IVerletShapeFactory
	{
		public void Createshape(Vector3 origin, out MassPoint[] massPoints, out Spring[] springs)
		{
			massPoints = new MassPoint[8];

			const float MassOfPointMasses = 1.0f;

			int p = 0;
			massPoints[p++] =
				new MassPoint(-1.0f + origin.x, 0.0f + origin.y, -1.0f + origin.z, MassOfPointMasses);
			massPoints[p++] = new MassPoint(-1.0f + origin.x, 0.0f + origin.y, 1.0f + origin.z, MassOfPointMasses);
			massPoints[p++] = new MassPoint(1.0f + origin.x, 0.0f + origin.y, 1.0f + origin.z, MassOfPointMasses);
			massPoints[p++] = new MassPoint(1.0f + origin.x, 0.0f + origin.y, -1.0f + origin.z, MassOfPointMasses);

			massPoints[p++] =
				new MassPoint(-1.0f + origin.x, 2.0f + origin.y, -1.0f + origin.z, MassOfPointMasses);
			massPoints[p++] = new MassPoint(-1.0f + origin.x, 2.0f + origin.y, 1.0f + origin.z, MassOfPointMasses);
			massPoints[p++] = new MassPoint(1.0f + origin.x, 2.0f + origin.y, 1.0f + origin.z, MassOfPointMasses);
			massPoints[p++] = new MassPoint(1.0f + origin.x, 2.0f + origin.y, -1.0f + origin.z, MassOfPointMasses);

			// Cross springs
			// 0 -> 6 // + 6
			// 1 -> 7 // + 6
			// 2 -> 4 // + 2
			// 3 -> 5 // + 2

			springs = new Spring[massPoints.Length * massPoints.Length];

			int s = 0;
			for (int i = 0; i < massPoints.Length; i++)
			{
				for (int j = 1; j < massPoints.Length; j++)
				{
					if (i != j && i < j)
					{
						springs[s++] = new Spring(massPoints, i, j, 1.0f);
					}
				}
			}
		}
	}

	public class VerletSim
	{
		private MassPoint[] _massPoint;

		private Spring[] Springs;

		public float ForceDueToGravity = 9.81f * 0.1f;

		public float SpringForce = 0.0125f;

		public float Damping = 1.0f;

		public float DrawPointScale = 0.2f;

		public void CreateSimulatedShape(Vector3 origin, IVerletShapeFactory factory)
		{
			factory.Createshape(origin, out _massPoint, out Springs);
		}

		public void Update(float deltaTime)
		{
			for (var m = 0; m < _massPoint.Length; m++)
			{
				var p = _massPoint[m].pos;
				Debug.DrawLine(p - Vector3.left * DrawPointScale, p + Vector3.left * DrawPointScale, Color.red);
				Debug.DrawLine(p - Vector3.up * DrawPointScale, p + Vector3.up * DrawPointScale, Color.green);
				Debug.DrawLine(p - Vector3.forward * DrawPointScale, p + Vector3.forward * DrawPointScale,
					Color.blue);

				Debug.DrawLine(p, p + (_massPoint[m].pos - _massPoint[m].previousPosition), Color.yellow);
			}

			for (var s = 0; s < Springs.Length; s++)
			{
				var extensionPercentage =
					(_massPoint[Springs[s].PointIndex2].pos - _massPoint[Springs[s].PointIndex1].pos).magnitude /
					Springs[s].RestLength;

				Debug.DrawLine(
					_massPoint[Springs[s].PointIndex1].pos,
					_massPoint[Springs[s].PointIndex2].pos,
					(Springs[s].Strength < 1.5f
						? Color.Lerp(Color.blue, Color.red, extensionPercentage * 0.5f)
						: Color.Lerp(Color.cyan, Color.magenta, extensionPercentage * 0.5f))
				);
			}
		}

		public void FixedUpdate(float fixedTime)
		{
			for (var m = 0; m < _massPoint.Length; m++)
			{
				_massPoint[m].accel = Vector3.down * ForceDueToGravity;
			}

			for (var s = 0; s < Springs.Length; s++)
			{
				FixedDistanceConstraint(
					Springs[s].PointIndex1,
					Springs[s].PointIndex2,
					Springs[s].RestLength,
					SpringForce * Springs[s].Strength,
					SpringForce * Springs[s].Strength
				);
			}

			for (var m = 0; m < _massPoint.Length; m++)
			{
				if (_massPoint[m].anchored)
				{
					_massPoint[m].accel = _massPoint[m].vel = Vector3.zero;
					_massPoint[m].pos = _massPoint[m].previousPosition;
				}
				else
				{
					_massPoint[m].vel = _massPoint[m].pos - _massPoint[m].previousPosition;
					_massPoint[m].previousPosition = _massPoint[m].pos;
					_massPoint[m].pos = _massPoint[m].pos +
					                     _massPoint[m].vel * Damping +
					                     _massPoint[m].accel * (fixedTime * fixedTime);
				}
			}

			for (var m = 0; m < _massPoint.Length; m++)
			{
				if (_massPoint[m].pos.y <= 0.0f)
				{
					_massPoint[m].pos.y = 0.0f;
				}
			}
		}

		void FixedDistanceConstraint(int p1, int p2, float distance, float? compensate1, float? compensate2)
		{
			var delta = _massPoint[p2].pos - _massPoint[p1].pos;
			var deltaLength = delta.magnitude;
			if (deltaLength > 0.0f)
			{
				var diff = (deltaLength - distance) / deltaLength;
				if (compensate1.HasValue)
				{
					_massPoint[p1].pos = _massPoint[p1].pos + delta * compensate1.Value * diff;
				}

				if (compensate2.HasValue)
				{
					_massPoint[p2].pos = _massPoint[p2].pos - delta * compensate2.Value * diff;
				}

				var adjustedDelta = delta = _massPoint[p2].pos - _massPoint[p1].pos;
				var adjustedDeltaLength = adjustedDelta.magnitude;
			}
		}
	}
}