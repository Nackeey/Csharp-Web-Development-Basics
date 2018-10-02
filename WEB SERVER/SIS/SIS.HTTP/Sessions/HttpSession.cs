namespace SIS.HTTP.Sessions
{
    using Sessions.Contracts;
    using System;
    using System.Collections.Generic;

    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> parameters;

        public HttpSession(string id)
        {
            this.Id = id;
            this.parameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException($"{name} cannot be null or empty!");
            }

            this.parameters.Add(name, parameter);
        }

        public void ClearParameters() => this.parameters.Clear();

        public bool ContainsParameter(string name) => this.parameters.ContainsKey(name);

        public object GetParameter(string name) => this.parameters.GetValueOrDefault(name, null);
    }
}
