namespace NaturalnieApp2.Main.Interfaces.Model
{
    public interface IModelDto<T>: IModel
    {
        public T? Model { get; set; }
    }
}
