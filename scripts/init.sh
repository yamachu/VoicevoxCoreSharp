#!/bin/bash

GIT_ROOT=$(git rev-parse --show-toplevel)
cp ${GIT_ROOT}/scripts/pre-commit.py ${GIT_ROOT}/.git/hooks/pre-commit; chmod +x ${GIT_ROOT}/.git/hooks/pre-commit
