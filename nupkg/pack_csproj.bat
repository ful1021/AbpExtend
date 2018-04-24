"NuGet.exe" "pack" "..\src\AbpExtend\AbpExtend.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"NuGet.exe" "pack" "..\src\AbpExtend.Web\AbpExtend.Web.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"NuGet.exe" "pack" "..\src\AbpExtend.Web.Api\AbpExtend.Web.Api.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"NuGet.exe" "pack" "..\src\AbpExtend.Web.SignalR\AbpExtend.Web.SignalR.csproj" -Properties Configuration=Release -IncludeReferencedProjects

pause
