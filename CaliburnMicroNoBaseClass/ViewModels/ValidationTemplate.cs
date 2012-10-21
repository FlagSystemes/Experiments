using System;
using System.ComponentModel;
using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace CaliburnMicroWithNoBaseClass.ViewModels
{
    public class ValidationTemplate : IDataErrorInfo
    {
        private INotifyPropertyChanged target;
        public object Target
        {
            get
            {
                return target;
            }
            set
            {
                target = (INotifyPropertyChanged)value;
                validator = ValidationFactory.GetValidator(target.GetType());
                validationResult = validator.Validate(Target);
                target.PropertyChanged += Validate;
            }
        }

        private void Validate(object sender, PropertyChangedEventArgs e)
        {
            if (validator != null)
            {
                validationResult = validator.Validate(target);
            }
        }

        private IValidator validator;
        private ValidationResult validationResult;

        public string Error
        {
            get
            {
                return string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
            }
        }

        public string this[string propertyName]
        {
            get
            {
                return string.Join(Environment.NewLine, validationResult.Errors.Where(x => x.PropertyName == propertyName).Select(x => x.ErrorMessage).ToArray());
            }
        }
    }
}