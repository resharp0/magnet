using QFramework;
public class LevelAr : Architecture<LevelAr>
{
    protected override void Init()
    {
        // ע�� Model
        this.RegisterModel(new LevelObjModel());
        this.RegisterModel(new CompareObj());
    }
}
