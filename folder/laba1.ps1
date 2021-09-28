Get-ChildItem | Where-Object { $_.Mode -eq "d-----"} | Sort-Object {[int]$_.Name} | Get-ChildItem -Filter *.* | Get-Content | Out-File result.txt
pause