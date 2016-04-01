# netRefTool

A simple Console app that you can use to reflect the dependencies on a given .net Assembly.  

## Usage

```
netRefTool FULL_PATH_TO_ASSEMBLY_WITH_EXTENSION [-v]
```

Also can get "verbose" output by passing the -v argument.

```
netRefTool F -v
```

You can also pipe filenames to it as well

```
dir *.dll /b /w /s | netRefTool
```

## Examples 

#### Non Verbose
```
C:\Windows\assembly\GAC_MSIL\System.Xml\2.0.0.0__b77a5c561934e089>netRefTool %cd%\System.XML.dll

References for: System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089

Assembly Name           Version         ProcessorArchitecture   culture
--------------------------------------------------------------------------------

mscorlib                4.0.0.0         None
System.Configuration    4.0.0.0         None
System.Data.SqlXml      4.0.0.0         None
System                  4.0.0.0         None
```

#### Verbose
```
C:\Windows\assembly\GAC_MSIL\System.Xml\2.0.0.0__b77a5c561934e089>netRefTool %cd%\System.XML.dll -v

References for: System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
System.Data.SqlXml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
```
