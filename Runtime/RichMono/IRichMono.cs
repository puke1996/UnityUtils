namespace Plugins.Puke.RichMono
{
    public interface IRichMono
    {
        public bool IsDestroyed { get; }
        public void EarlyUpdate();
        public void FinallyUpdate();
    }
}