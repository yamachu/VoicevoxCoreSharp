# 開発者デバッグ用

publish/aot/osx-arm64:
	dotnet publish -r osx-arm64 -c Release

validate/memory/osx-arm64: RESULT=result.txt
validate/memory/osx-arm64:
	leaks --atExit -- ./bin/Release/net8.0/osx-arm64/publish/cli Hello > $(RESULT)

run/.local: TEXT=
run/.local:
	dotnet run "$(TEXT)" --resource=../../.local/voicevox_core -p CoreBasePath=../../.local/voicevox_core
