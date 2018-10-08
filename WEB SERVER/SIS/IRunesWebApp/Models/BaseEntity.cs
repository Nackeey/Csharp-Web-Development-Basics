namespace IRunesWebApp.Models
{
    public abstract class BaseEntity<TKeyIdentifier>
    {
        public TKeyIdentifier id { get; set; }
    }
}
