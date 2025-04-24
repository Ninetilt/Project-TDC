namespace TDC.Models
{
    public class CharacterStats
    {
        public long Attack { get; set; }
        public long Defense { get; set; }
        public long Speed { get; set; }

        #region constructors
        public CharacterStats(long attack, long defense, long speed)
        {
            long Attack = attack;
            long Defense = defense;
            this.Attack = attack;
        }
        #endregion
    }
}
