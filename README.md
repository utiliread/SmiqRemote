This repository contains a remote control server and client for the [Rohde&Schwarz SMIQ Vector Signal Generator](https://cdn.rohde-schwarz.com/pws/dl_downloads/dl_common_library/dl_manuals/gb_1/s/smiq_1/Smiqb_OperatingManual_V1_en_11.pdf). The server should be connected to the device using the RS232 interface, and will then provide a REST api for the available features.

The client is web-based and [the latest version is available online here](https://utiliread.github.io/SmiqRemote/). Note that "insecure scripts" should be enabled in the brower if the server is hosted using http without a certificate.

## Server Installation ##
Install the .NET runtime using the [official instructions](https://www.microsoft.com/net/download/linux-package-manager/ubuntu17-10/runtime-2.0.6).
Continue by installing the runtime store:

```
sudo apt-get install dotnet aspnetcore-store-2.0.6
```

[Configure the hosting environment](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?tabs=aspnetcore2x)

Follow the instructions on how to compile and install libnserial from the [SerialPortStream github page](https://github.com/jcurl/SerialPortStream#linux).
Remember to update the library cache with 

```
sudo ldconfig
```
