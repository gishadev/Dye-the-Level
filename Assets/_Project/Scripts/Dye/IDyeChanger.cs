namespace Gisha.DyeTheLevel.Dye
{
    public interface IDyeChanger
    {
        bool ContainsDyeSample(IColorable colorable, out IDyeSample sample);
        void Color(IColorable colorable, IDyeSample newSample, IDyeSample previousSample);
        void Discolor(IColorable colorable, IDyeSample previousSample);
    }
}