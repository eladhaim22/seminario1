namespace Seminario.NHibernate
{
    using System;
    public sealed class Synchronization : ISynchronization
    {
        public Action BeforeCompletion
        {
            get;
            set;
        }
        
        public Action<bool> AfterCompletion
        {
            get;
            set;
        }
        
        void ISynchronization.BeforeCompletion()
        {
            if (this.BeforeCompletion != null)
            {
                this.BeforeCompletion();
            }
        }

        void ISynchronization.AfterCompletion(bool committed)
        {
            if (this.AfterCompletion != null)
            {
                this.AfterCompletion(committed);
            }
        }
    }
}
