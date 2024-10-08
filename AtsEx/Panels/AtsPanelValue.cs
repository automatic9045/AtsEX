﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AtsEx.PluginHost;
using AtsEx.PluginHost.Binding;
using AtsEx.PluginHost.Panels.Native;

namespace AtsEx.Panels
{
    internal class AtsPanelValue<T> : IAtsPanelValue<T>
    {
        private readonly ITwoWayConverter<T, int> ValueSerializer;

        private T _Value;
        public T Value
        {
            get => _Value;
            set
            {
                if (value.Equals(Value)) return;

                _Value = value;
                SerializedValue = ValueSerializer.Convert(value);

                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int SerializedValue { get; private set; }

        public BindingMode Mode { get; set; }

        public event EventHandler ValueChanged;

        public AtsPanelValue(T initialValue, ITwoWayConverter<T, int> valueSerializer, BindingMode mode)
        {
            ValueSerializer = valueSerializer;

            Value = initialValue;
            Mode = mode;
        }

        public void Dispose()
        {
        }

        public void SetValueExternally(int source) => Value = ValueSerializer.ConvertBack(source);
    }
}
