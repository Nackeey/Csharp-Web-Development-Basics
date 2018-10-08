namespace SIS.HTTP.Sessions
{
    using Sessions.Contracts;
    using System.Collections.Generic;

    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> sessionParameters;

        public HttpSession(string id)
        {
            this.Id = id;
            this.sessionParameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public object GetParameter(string name)
        {
            return this.sessionParameters.GetValueOrDefault(name, null);
        }

        public bool ContainsParameter(string name)
        {
            return this.sessionParameters.ContainsKey(name);
        }

        public void AddParameter(string name, object parameter)
        {
            if (!this.sessionParameters.ContainsKey(name))
            {
                this.sessionParameters.Add(name, parameter);
            }
        }

        public void DeleteParameter(string name)
        {
            this.sessionParameters.Remove(name);
        }

        public void ClearParameters()
        {
            this.sessionParameters.Clear();
        }
    }
}
