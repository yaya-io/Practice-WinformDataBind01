using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Practice_WinformDataBind01
{
    public partial class Form1 : Form
    {
        private BindingList<Book> _bindingBookList;
        private MyCondition _condition;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //一覧部のバインディング  
            _bindingBookList = new BindingList<Book>()
            {
                new Book(id:"A",name:"書籍１"),
                new Book(id:"B",name:"小説１")
            };

            this.dgv.DataSource = _bindingBookList;

            //条件部のバインディング  
            _condition = new MyCondition();
            this.txtId.DataBindings.Add(new Binding(nameof(this.txtId.Text), _condition, nameof(_condition.Id)));
            this.txtName.DataBindings.Add(new Binding(nameof(this.txtId.Text), _condition, nameof(_condition.Name)));

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (_condition.Id == "A" && _condition.Name == "書籍１")
            {
                MessageBox.Show("存在する!");
            }
            else
            {
                _condition.Id = "Z";
                _condition.Name = "テスト";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _bindingBookList.Add(new Book("X", "書籍２"));
        }

    
    }

    class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Book(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }

    class MyCondition : BindableBase
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { this.SetProperty(ref this.id, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetProperty(ref this.name, value); }
        }

    }

    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(field, value)) { return false; }
            field = value;
            var h = this.PropertyChanged;
            if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
            return true;
        }
    }
}
