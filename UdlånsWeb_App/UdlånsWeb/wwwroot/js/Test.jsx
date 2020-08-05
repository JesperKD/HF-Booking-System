class HelloMessage extends React.Component {
    render() {
        return (
            <div>
                Hello {this.props.name}
            </div>
        );
    }
}

reactDOM.render(
    <HelloMessage name="Lars" />,
    document.getElementById('hello-example')
);