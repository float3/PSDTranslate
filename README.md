# PSDTranslate

Translates the layer names of `.psd` files to English using Google Translate. C# rewrite of [psd_translate](https://github.com/float3/psd_translate); reads files with Aspose.PSD and translates through the bundled [GoogleTranslate.NET](https://github.com/float3/GoogleTranslate.NET) submodule.

## Build

```sh
git clone --recurse-submodules https://github.com/float3/PSDTranslate
dotnet build -c Release
```

## Usage

```sh
PSDTranslate path/to/files/*.psd
```

Without arguments it translates every `.psd` in the current directory.
