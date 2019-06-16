namespace SimonsSearch.Service.Constants
{
    public class PropertyDefinition
    {
        public PropertyDefinition(string propertyName, string propertyObject)
        {
            PropertyName = propertyName;
            PropertyObject = propertyObject;
        }

        public string PropertyName { get; private set; }
        public string PropertyObject { get; private set; }

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode()+PropertyObject.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as PropertyDefinition);
        }

        public bool Equals(PropertyDefinition obj)
        {
            return obj != null && obj.PropertyName == PropertyName && obj.PropertyObject == PropertyObject;
        }
    }
}
