namespace Ladeskab.Interfaces
{
    public interface IChargeControl
    {
        public bool Connected { get;  }
        public void StartCharge();

        public void StopCharge();
    }
}