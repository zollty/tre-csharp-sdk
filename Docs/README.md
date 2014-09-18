---
layout: docs
title: C# SDK 使用指南
---

# C# SDK 使用指南

此 C# SDK 适用于.net framework>2.0版本，基于中国航信报表引擎（TRE）官方API构建。使用此 SDK 构建您的网络应用程序，能让您以非常便捷地使用报表引擎的服务。无论您的网络应用是一个网站程序，还是包括从云端（服务端程序）到终端（手持设备应用）的架构的服务或应用，通过中国航信报表引擎及其 SDK，都能让您应用程序的终端用户高速查询和下载，同时也让您的服务端更加轻盈。

- [安装](#install)
- [初始化](#setup)
	- [配置密钥](#setup-key)
- [下载报表](#rs-api)
	- [下载周期报表](#rs-downcyc)
- [贡献代码](#contribution)
- [许可证](#license)

<a name=install></a>
##  安装

NuGet安装方式：

	如果您的Visual Studio没有安装NuGet，请先[安装](http://docs.nuget.org/docs/start-here/installing-nuget)它,然后在项目中添加引用，使用NuGet管理程序包。

源码下载:

	git clone https://github.com/TravelskyTech/tre-csharp-sdk

DLL引用方式:

	下载DLL文件，右键<项目>-<引用>文件夹，在弹出的菜单中点击"添加引用"选项后弹出"添加引用"对话框，选择”浏览"TRE.DLL文件,点击确定	

项目引用方式：

	下载项目文件，右键解决方案，在弹出的菜单中点击"添加"->"现有项目"，然后在弹出的对话框中选择 TRE.csproj"文件，点击确定。接下来与DLL引用方式类似，在"添加引用”对话框选择"项目"选项卡后选中TRE项目即可。

其它:

	C# SDK引用了第三方的开源项目 Common.Logging，因此，您需要在项目中引用它(如果使用NuGet管理dll，刚不需要手工加载Common.Logging.dll)。

<a name=setup></a>
## 初始化
<a name=setup-key></a>
### 配置密钥

客户端使用。

要接入中国航信报表引擎（TRE），您需要拥有一对有效的 Access Key 和 Secret Key 用来进行签名认证。可以通过如下步骤获得：

1. 向中国航信申请使用报表引擎的服务，航信将会提供给您一套配置（包括账户和密匙）。

在获取到 Access Key 和 Secret Key 之后，您可以在您的程序中配置并初始化对接, 要确保`baseURL` 和 `appSecret` 等在<u>调用所有TRE API服务之前均已赋值</u>：

比如，编译配置文件app.conf或者web.conf等文件，添加以下配置项：

``` xml
<appSettings>
    <add key="baseURL" value="http://123.100.123.25/report/" />
    <add key="appid" value="fdaac839a31haf4fda03ab4ce5ef326b00" />
    <add key="appSecret" value="4fa01e450a894ce78bfd430ed3bdf12" />
    <add key="systemid" value="0001" />
    <add key="XXB2B" value="acbdef7k00"/>
    <add key="XQB2B" value="68g93kkf00"/>
</appSettings>
```

<a name=rs-api></a>
## 下载报表

具体用法参见 [https://github.com/TravelskyTech/tre-csharp-sdk] 下面的TRE.Test的例子：

```c#
/**
 * 下载指定航空公司的销售报表
 * @param airlineCode 航空公司代码 例如HUB2B(海航) BKB2B(奥凯航) DRB2B(瑞丽航) 详见 @see DownloadReport
 */
public static void testdownloadSaleReport(String airlineCode)
{
	try
	{
		// 本地存储文件路径 
		String localFilePath = "D:/";
		
		// 注意，返回的是一个压缩过的zip文件
		String localFileName = "rpt" + "-sale-" + RandomUtils.getRadomStr09AZaz(10) + ".zip";

		// 调用该API下载报表
		String fileName = DownloadReport.downloadSaleReport(localFilePath, localFileName, airlineCode, DownloadReport.ZH_CN);
		
		log.Info(airlineCode + "销售报表下载成功，下载到的文件名: " + fileName);
	}
	catch (TreClientException e)
	{
	    // 出错后可以获取TRE服务端返回的错误信息
		log.Error("错误代码：" + e.ErrorCode);
		log.Error("错误信息：" + e.Message);
	}
}
```

<a name=contribution></a>
## 贡献代码

1. Fork
2. 创建您的特性分支 (`git checkout -b my-new-feature`)
3. 提交您的改动 (`git commit -am 'Added some feature'`)
4. 将您的修改记录提交到远程 `git` 仓库 (`git push origin my-new-feature`)
5. 然后到 github 网站的该 `git` 远程仓库的 `my-new-feature` 分支下发起 Pull Request

<a name=license></a>
## 许可证

Copyright (c) 2014 Travelsky

基于 Apache License, Version 2.0 协议发布:

* [http://www.apache.org/licenses/LICENSE-2.0]
