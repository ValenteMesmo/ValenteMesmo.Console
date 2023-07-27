using System.IO;
using System.Text;
using System;

namespace ValenteMesmo
{
    public class StringWriterStream : Stream
    {
        private readonly StringWriter writer;

        public StringWriterStream(StringWriter writer)
        {
            this.writer = writer;
        }

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;

        public override long Length => throw new NotSupportedException();
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override void Flush() => writer.Flush();

        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count)
        {
            writer.Write(Encoding.UTF8.GetString(buffer, offset, count));
        }
    }
}