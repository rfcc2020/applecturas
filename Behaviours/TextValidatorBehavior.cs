﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AppTesisLecturas.Behaviours
{
    //clase para validar que un control reciba solo caracteres alfabéticos
    public class TextValidatorBehavior : Behavior<Entry>
    {
        const string digitosRegEx = @"^([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\']+[\s])+([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])+[\s]?([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])?$";

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += TextChanged;
            base.OnAttachedTo(entry);
        }

        // Solo texto
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            bool valido = (Regex.IsMatch(e.NewTextValue, digitosRegEx, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = valido ? Color.Green : Color.Red;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
