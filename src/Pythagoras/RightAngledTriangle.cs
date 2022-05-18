namespace Pythagoras
{
    public class RightAngledTriangle
    {
        public IReadOnlyDictionary<Properties, float> Properties => new Dictionary<Properties, float>(properties);

        private Dictionary<Properties, float> properties { get; }
        public float this[int index]
        {
            get { return properties.Values.ToArray()[index]; }
        }

        public RightAngledTriangle()
        {
            properties = new Dictionary<Properties, float>();
        }

        public void AddProperty(Properties property, float value)
        {
            properties.Add(property, (float)Math.Round(value, 2));
        }

        public void AddProperty(Properties property, double value)
        {
            properties.Add(property, (float)Math.Round(value, 2));
        }
    }

    public enum Properties
    {
        a,
        b,
        c,
        p,
        q,
        h,
        A
    }


}
