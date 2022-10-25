using QFramework;
public class LevelAr : Architecture<LevelAr>
{
    protected override void Init()
    {
        // зЂВс Model
        this.RegisterModel(new LevelObjModel());
        this.RegisterModel(new CompareObj());
    }
}
