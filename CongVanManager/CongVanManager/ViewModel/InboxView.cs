namespace CongVanManager
{
    internal class InboxView
    {
        private int _amountOfLetters = 1337;
        public int AmountOfLetters
        {
            get { return _amountOfLetters; }
            set { _amountOfLetters = value; }
        }

        public InboxView()
        {
        }
    }
}