using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TemperatureService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TemperatureService : ITemperatureService, IDisposable
    {
        private readonly List<string> _exceptionsLog = new List<string>();
        private readonly object _lock = new object();
        private static List<TemperatureMessage> Temperatures = new List<TemperatureMessage>();
        public TemperatureService()
        {
          /*  try
            {
                this.TemperatureContext = new TemperatureContext();
                this.TemperatureContext.Database.CreateIfNotExists();
                this.TemperatureContext.Database.Connection.Open();

                this.TemperatureContext.TemperatureMessages.Add(new TemperatureMessage() { TimeStamp = DateTime.Now, Temperature = "test", Sender = "test1" });
                this.TemperatureContext.TemperatureMessages.Add(new TemperatureMessage() { TimeStamp = DateTime.Now, Temperature = "test1", Sender = "test1" });
                this.TemperatureContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                this._exceptionsLog.Add(ex.ToString());
            }
            */
        }
        public TemperatureContext TemperatureContext { get; set; }

        public void ClearTemperature()
        {
            Temperatures.Clear();
            /* lock (this._lock)
             {
                 this.TemperatureContext?.TemperatureMessages.RemoveRange(this.TemperatureContext.TemperatureMessages);
                 this.TemperatureContext?.SaveChanges();
             }*/
        }

        public void Dumy() { }
        public List<string> ExceptionsLog() { lock (this._lock) { return this._exceptionsLog; } }

        public List<TemperatureMessage> GetLastDayTemperatures()
        {
            List<TemperatureMessage> RetTemperatures = new List<TemperatureMessage>();
            foreach (var p in Temperatures)
                if (p.TimeStamp > DateTime.Now.AddDays(-1))
                {
                    RetTemperatures.Add(p);
                }
            return RetTemperatures;
        }

        public List<TemperatureMessage> GetAllTemperatures()
        {
            return Temperatures;
           /* lock (this._lock)
            {
                return this.TemperatureContext?.TemperatureMessages.ToList() ?? new List<TemperatureMessage>();
            }*/
        }

        public void SaveTemperature(TemperatureMessage temperatureMessage)
        {
            if (temperatureMessage == null) { throw new ArgumentNullException("temperatureMessage"); }
            if (temperatureMessage.TimeStamp == DateTime.MinValue) { temperatureMessage.TimeStamp = DateTime.Now; }
            Temperatures.Add(temperatureMessage);
            /*lock (this._lock)
            {
                this.TemperatureContext?.TemperatureMessages.Add(temperatureMessage);
                this.TemperatureContext?.SaveChanges();
            }*/

        }

        #region Implementation of IDisposable
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.TemperatureContext?.Database.Connection.Close();
            this.TemperatureContext = null;
        }
        #endregion
    }
}