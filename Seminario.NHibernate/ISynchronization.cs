namespace Seminario.NHibernate
{
    public interface ISynchronization
    {
        void BeforeCompletion();
        void AfterCompletion(bool committed);
    }
}
