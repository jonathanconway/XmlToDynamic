#  XmlToDynamic

XmlToDynamic converts your XML object into a hierarchy of nested objects, so you can get at values and attributes more easily.

Instead of this:

    xelement.Element(XName.Get("contact")).Attribute(XName.Get("firstName")).Value

You can type this:
	
	xelement.contact.Attributes["firstName"]

The object returned is also dynamic, so you can add new properties on-the-fly.

## Usage

To dynamize your XML, simply generate an XElement of it, then call `ToDynamic()` on it.

	var xmlString = @"<?xml version=""1.0"" encoding=""UTF-8""?><blah></blah>";
    var dynamicXml = XElement.Parse(xmlString).ToDynamic();

### Values

The elements get converted to properties, and their values can be accessed through the `Value` property.

	// <addressBook>
	//   <contacts>
	//     <contact>
	//       <name>Jonathan</name>
	//     </contact>
	//   </contacts>
	// </addressBook>
    var firstPersonsName = dynamicXml.contacts[0].name.Value;
    // firstPersonsName will be 'Jonathan'

### Attributes

Attributes can be access through the `Attributes` property, which is a string-string dictionary.

	// <addressBook>
	//   <contacts>
	//     <contact id="1">
	//       <name>Jonathan</name>
	//     </contact>
	//   </contacts>
	// </addressBook>
    var firstPersonsId = dynamicXml.contacts[0].Attributes["id"];
    // firstPersonsId will be '1'

### Repeated elements

Whenever repeated elements are encountered, they're put into a container which is named after the element names pluralized.

	// <colors>
	//   <blue></blue>
	//   <green lightness="1"></green>
	//   <green lightness="10"></green>
	// </colors>
    var darkGreenLightness = dynamicXml.greens[0].Attributes["lightness"];
    var lightGreenLightness = dynamicXml.greens[1].Attributes["lightness"];
    // darkGreenLightness will be '1'
    // lightGreenLightness will be '10'