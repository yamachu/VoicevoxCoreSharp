#!/usr/bin/python3

import subprocess

src_root = 'src/VoicevoxCoreSharp.Core/'
unity_root = 'src/VoicevoxCoreSharp.Core.Unity/Runtime/Script/'

process_result = subprocess.run("git diff --staged --name-status | grep '.cs$'", capture_output=True, shell=True, encoding="utf-8")

modified_files = set()
added_files = set()
deleted_files = set()
renamed_files = set()

for diff in process_result.stdout.strip().splitlines():
    file_name = diff.split()[1]
    trimmed_file_name = file_name.removeprefix(src_root).removeprefix(unity_root)
    if diff.startswith('M') and (file_name.startswith(src_root) or file_name.startswith(unity_root)):
        if trimmed_file_name in modified_files:
            modified_files.remove(trimmed_file_name)
        else:
            modified_files.add(trimmed_file_name)
    elif diff.startswith('A') and (file_name.startswith(src_root) or file_name.startswith(unity_root)):
        if trimmed_file_name in added_files:
            added_files.remove(trimmed_file_name)
        else:
            added_files.add(trimmed_file_name)
    elif diff.startswith('D') and (file_name.startswith(src_root) or file_name.startswith(unity_root)):
        if trimmed_file_name in deleted_files:
            deleted_files.remove(trimmed_file_name)
        else:
            deleted_files.add(trimmed_file_name)
    elif diff.startswith('R') and (file_name.startswith(src_root) or file_name.startswith(unity_root)):
        if trimmed_file_name in renamed_files:
            renamed_files.remove(trimmed_file_name)
        else:
            renamed_files.add(trimmed_file_name)

error_count = 0

if len(added_files) > 0:
    print('Added cs files detected, please sync Unity project.')
    error_count += 1
if len(deleted_files) > 0:
    print('Deleted cs files detected, please sync Unity project.')
    error_count += 1
if len(renamed_files) > 0:
    print('Renamed cs files detected, please sync Unity project.')
    error_count += 1
if len(modified_files) > 0:
    print('Modified cs files detected, auto-sync Unity project.')
    for f in modified_files:
        subprocess.run(f"cp {src_root}{f} {unity_root}{f}; git add {unity_root}{f}", shell=True)

exit(1 if error_count > 0 else 0)
