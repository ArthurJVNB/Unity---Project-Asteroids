using UnityEngine;

namespace Project
{
	[System.Serializable]
	public struct ScoreData
	{
		public int ScoreTotal;
		public int ScoreIncrement;
		public Vector2 AsteroidPosition;

		public ScoreData(int scoreTotal, int scoreIncrement, Vector2 asteroidPosition)
		{
			ScoreTotal = scoreTotal;
			ScoreIncrement = scoreIncrement;
			AsteroidPosition = asteroidPosition;
		}
	}
}
