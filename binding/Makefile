.PHONY: generate replace-hash build/dev-library

generate:
	cargo build
	$(MAKE) replace-hash

replace-hash:
	$(eval VERSION := $(shell cat ../.git/modules/binding/voicevox_core/HEAD))
	$(shell sed -i '.bak' 's/<VoicevoxCoreCommitHash>.*<\/VoicevoxCoreCommitHash>/<VoicevoxCoreCommitHash>$(VERSION)<\/VoicevoxCoreCommitHash>/' ../src/VoicevoxCoreSharp.Core/VoicevoxCoreSharp.Core.Metas.props)

build/dev-library:
	cd voicevox_core; cargo build --release -p voicevox_core_c_api
