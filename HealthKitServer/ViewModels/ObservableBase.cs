using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace HealthKitServer
{
	public abstract class ObservableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void SetPropertyValue<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(property, value))
			{
				return;
			}

			property = value;
			this.NotifyPropertyChanged(propertyName);
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			var propertyChangedEvent = this.PropertyChanged;
			if (propertyChangedEvent != null)
			{
				propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
