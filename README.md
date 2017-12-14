# heyzapAds-unity-plugin

Instructions:
	Import Package
	Assets > Play Services Resolver > Android Resolver  Resolve Client Jars
	Enable Unity ads   // For Google Play only
	
	Wise Wing Games > General Settings  // Fill it
	Click Set bundle identifier button
	
	Wise Wing Games > Ads Settings      //Fill it
	
	
	SHOWING ADS:
	
		Add ShowAdOnEnable.cs on the UIPanel/GameObject to show intersitial ad when its enabled   //Set adTag similar to adSettings 
	
		Add ShowBanner.cs in the scene to show banner ad.
	
		Add ShowRewardAd.cs on the UIButton to show reward ad //Assign GameObject to recieve callback 