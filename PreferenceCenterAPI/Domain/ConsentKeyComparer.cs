namespace PreferenceCenterAPI.Domain
{
    public class ConsentKeyComparer : IComparer<Consent>
    {
        private bool _desc;

        public ConsentKeyComparer(bool desc = false)
        {
            _desc = desc;
        }
        public int Compare(Consent? x, Consent? y)
        {
            if (_desc)
                return y.Key.CompareTo(x.Key);
            else
                return x.Key.CompareTo(y.Key);
        }
    }
}
