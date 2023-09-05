# 開発者デバッグ用

publish/aot/osx-arm64:
	dotnet publish -r osx-arm64 -c Release

validate/memory: RESULT=result.txt
validate/memory:
	leaks --atExit -- ./bin/Release/net8.0/osx-arm64/publish/cli Hello > $(RESULT)
