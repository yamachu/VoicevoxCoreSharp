.PHONY: format

format:
	dotnet format VoicevoxCoreSharp-without-exmaples.slnf

format/check:
	dotnet format VoicevoxCoreSharp-without-exmaples.slnf --verify-no-changes
