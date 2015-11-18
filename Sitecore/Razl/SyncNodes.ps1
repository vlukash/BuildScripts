# list all root items for sync here
$rootItems = "6AF71A22-9A9A-4DAF-AD0E-A2B445599164", # /sitecore/content/Global
             "6030F735-14B9-4B6F-A493-EF8B714581FD", # /sitecore/content/MySite1/Home
             "0A718DB5-0AEC-439E-9064-D8B68517C5A2", # /sitecore/content/MySite1/Products
             "992F1187-5E32-4C35-BF95-390E8F4EB994", # /sitecore/content/MySite2/Home
             "EA63C156-D879-4EF3-9382-2B1011EDDD77", # /sitecore/content/MySite2/Products
			 "15451229-7534-44EF-815D-D93D6170BFCB" # /sitecore/media library/Images

foreach ($rootItem in $rootItems) {
    Write-Host Root node: $rootItem
    razl /script:.\SyncNodeCM2UAT.xml /p:itemId=$rootItem
}
