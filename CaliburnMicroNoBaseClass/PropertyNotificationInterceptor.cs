using System;
using System.Windows;

namespace CaliburnMicroWithNoBaseClass
{
    public static class PropertyNotificationInterceptor
    {
        public static void Intercept(object target, Action onPropertyChangedAction, string propertyName)
        {
            Application.Current.Dispatcher.Invoke(onPropertyChangedAction);
        }
    }
}