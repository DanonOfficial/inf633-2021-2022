public class SmoothBrush : TerrainBrush
{
    public float incrementSize = 1;
    public float smoothCoefficient = 0.1f;
    public override void draw(int x, int z)
    {
        float meanValue = 0;
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float currentValue = terrain.get(x + xi, z + zi);
                meanValue += currentValue;
                //terrain.set(x + xi, z + zi, currentValue + incrementSize);
            }
        }
        meanValue /= (4 * radius * radius);
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++) 
            {
                float currentValue = terrain.get(x + xi, z + zi);
                terrain.set(x + xi, z + zi, currentValue + (meanValue - currentValue) * smoothCoefficient);
            }
        }

    }
}
