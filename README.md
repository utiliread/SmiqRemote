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