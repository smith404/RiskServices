using System.Reflection;
using System.Text;

namespace FRMObjects.model
{
    public class BaseModelObject
    {
        private PropertyInfo[]? _PropertyInfos = null;

        public override string ToString()
        {
            if (_PropertyInfos == null)
            {
                _PropertyInfos = GetType().GetProperties();
            }

            StringBuilder sb = new StringBuilder();

            foreach (PropertyInfo info in _PropertyInfos)
            {
                object value = info.GetValue(this, null) ?? "(null)";
                _ = sb.AppendLine($"{info.Name}: {value}");
            }

            return sb.ToString();
        }
    }
}
