namespace NaturalnieApp2.Main.Interfaces.Model
{
    public interface IConvertableModel<T>
    {
        void FromModel(T model);
        T ToModel();
    }
}
