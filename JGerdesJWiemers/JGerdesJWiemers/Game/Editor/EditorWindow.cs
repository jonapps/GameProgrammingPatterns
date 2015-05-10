using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JGerdesJWiemers.Game.Editor
{
    partial class EditorWindow : Form
    {
        private EditorScreen _es;
        public EditorWindow(EditorScreen es)
        {
            InitializeComponent();
            _es = es;
        }

        private void load_Click(object sender, EventArgs e)
        {
            _es.LoadSprite(path.Text, (int)width.Value, (int)height.Value);
            result.Text = "";
        }

        public void SetResult(String text)
        {
            result.Text = text;
        }

        private void clipboard_Click(object sender, EventArgs e)
        {
            XClipboard.setText(result.Text);
        }

        private void reset_Click(object sender, EventArgs e)
        {
            result.Text = "";
            _es.ResetShape();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _es.ToggleAnimation(checkBox1.Checked);
        }
    }
}


// from http://www.mycsharp.de/wbb2/thread.php?threadid=15202
public class XClipboard
{
    public static void setText(string text)
    {
        XClipboard x = new XClipboard(text);

        Thread t = new Thread(new ThreadStart(x.Dummy));
        t.ApartmentState = ApartmentState.STA;
        t.Start();
    }

    private XClipboard(string Text)
    {
        m_Text = Text;
    }

    private void Dummy()
    {
        System.Windows.Forms.Clipboard.SetText(m_Text);
    }

    private string m_Text;
}
