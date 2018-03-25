namespace Easy_Http
{
    [System.AttributeUsage(System.AttributeTargets.Property |
        System.AttributeTargets.Field)]
    public class EndpotinContract : System.Attribute
    {
        public bool PutInRequest;
    }
}
