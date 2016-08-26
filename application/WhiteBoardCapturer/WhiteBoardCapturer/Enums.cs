namespace WhiteBoardCapturer
{
    enum ImageIndex
    {
        SOURCE      = 0,
        DESTINATION = 1
    }

    enum FileFilterIndex
    {   
        JPEG      = 1,
        PNG       = 2,
        ALL_FILES = 3,
    }

    public enum ApplicationState
    {
        IDLE,
        START_UP,
        SHUT_DOWN,
        FILE_OPEN,
        FILE_CLOSED,
        IMAGE_PROCESSING_IN_PROGRESS,
        IMAGE_PROCESSED,
    }
}
