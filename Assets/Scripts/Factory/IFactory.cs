namespace Asteroids.Factory
{
    public interface IFactory { }
    
    public interface IFactory<TObject> : IFactory
    {
        TObject Create();
        TObject Create(TObject obj);
    }

    public interface IFactory<TObject, in TArg1> : IFactory
    {
        TObject Create(TArg1 arg1);
        TObject Create(TObject obj, TArg1 arg1);
    }
    
    public interface IFactory<TObject, in TArg1, in TArg2> : IFactory
    {
        TObject Create(TArg1 arg1, TArg2 arg2);
        TObject Create(TObject obj, TArg1 arg1, TArg2 arg2);
    }
    
    public interface IFactory<TObject, in TArg1, in TArg2, in TArg3> : IFactory
    {
        TObject Create(TArg1 arg1, TArg2 arg2, TArg3 arg3);
        TObject Create(TObject obj, TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }
    
    public interface IFactory<TObject, in TArg1, in TArg2, in TArg3, in TArg4> : IFactory
    {
        TObject Create(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
        TObject Create(TObject obj, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
    }
}