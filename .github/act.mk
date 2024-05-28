test/merge-queue-trigger-handler:
	act -W ./workflows/merge-queue-trigger-handler.yml -e ./fixtures/merge-queue-trigger-handler-event-sample.json -s GITHUB_TOKEN=$$(gh auth token)
