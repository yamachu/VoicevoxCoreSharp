.PHONY: format

format:
	dotnet format VoicevoxCoreSharp.sln

format/check:
	dotnet format VoicevoxCoreSharp.sln --verify-no-changes
