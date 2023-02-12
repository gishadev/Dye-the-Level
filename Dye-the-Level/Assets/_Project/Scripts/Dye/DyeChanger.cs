namespace Gisha.DyeTheLevel.Dye
{
    public class DyeChanger : IDyeChanger
    {
        private readonly IDyeManager _dyeManager;

        public DyeChanger(IDyeManager dyeManager)
        {
            _dyeManager = dyeManager;
        }

        public bool ContainsDyeSample(IColorable colorable, out IDyeSample sample)
        {
            sample = null;

            foreach (var dye in _dyeManager.Samples)
            {
                if (dye == colorable.CurrentDye)
                {
                    sample = dye;
                    return true;
                }
            }

            return false;
        }

        public void Color(IColorable colorable, IDyeSample newSample, IDyeSample previousSample)
        {
            previousSample?.AddCount(1);

            colorable.ApplyDye(newSample);
            newSample.AddCount(-1);
        }

        public void Discolor(IColorable colorable, IDyeSample previousSample)
        {
            previousSample?.AddCount(1);
            colorable.RemoveDye();
        }
    }
}