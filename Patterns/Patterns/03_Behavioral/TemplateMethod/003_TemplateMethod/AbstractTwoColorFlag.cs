namespace Patterns._03_Behavioral.TemplateMethod._003_TemplateMethod
{
    abstract class TwoColorFlag
    {
        public void Draw()
        {
            DrawTopPart();
            DrawBottomPart();
        }

        protected abstract void DrawTopPart();
        protected abstract void DrawBottomPart();
    }
}
