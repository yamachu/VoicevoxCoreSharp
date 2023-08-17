.PHONY: generate replace-hash

generate:
	cargo build
	$(MAKE) replace-hash

replace-hash:
	$(eval VERSION := $(shell cat ../.git/modules/binding/voicevox_core/$(shell cat ../.git/modules/binding/voicevox_core/HEAD | cut -d' ' -f2)))
	$(shell sed -i '.bak' 's/<VoicevoxCoreCommitHash>.*<\/VoicevoxCoreCommitHash>/<VoicevoxCoreCommitHash>$(VERSION)<\/VoicevoxCoreCommitHash>/' ../src/VoicevoxCoreSharp.Core/VoicevoxCoreSharp.Core.Metas.props)
