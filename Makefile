.PHONY: format

format:
	dotnet format VoicevoxCoreSharp-without-exmaples.slnf

format/check:
	dotnet format VoicevoxCoreSharp-without-exmaples.slnf --verify-no-changes

submodule/update:
	git submodule update --remote

submodule/checkout: COMMIT=
submodule/checkout:
	@if [[ "$(COMMIT)" -eq "" ]]; then echo Needs "COMMIT args" ; exit 1 ; fi
	$(MAKE) submodule/update
	git submodule foreach git checkout $(COMMIT)
