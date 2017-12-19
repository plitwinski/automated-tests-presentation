import React, { Component } from 'react'
import PropTypes from 'prop-types'
import ReactDOM from 'react-dom'
import { createStore } from 'redux'
import { Provider, connect } from 'react-redux'
import axios from 'axios'

//example config, could be loaded from external file
const config = {
  apiUrl: "http://127.0.0.1:3000"
}


// React component
class Movies extends Component {
  render() {
    const { items, onLoadClick } = this.props
    const listItems = items.map((item) => <li>{item.title}, {item.director}</li>)
    return (
      <div>
        <button onClick={onLoadClick}>Load data</button>
        <ul>{listItems}</ul>
      </div>
    )
  }
}

Movies.propTypes = {
  items: PropTypes.array.isRequired,
  onLoadClick: PropTypes.func.isRequired
}

const loadDataAction = 'loadData'

const httpClient = axios.create({ baseURL : config.apiUrl })

const loadData = () => httpClient.get('/movies').then(response => response.data)

// Reducer
const moviesReducer = (state = { items: [] }, action) => {
  switch (action.type) {
    case loadDataAction:
      return { items: action.data }
    default:
      return state
  }
}

// Store
const store = createStore(moviesReducer)

// Map Redux state to component props
function mapStateToProps(state) {
  return {
    items: state.items
  }
}

// Map Redux actions to component props
function mapDispatchToProps(dispatch) {
  return {
    onLoadClick: () => loadData().then(response => dispatch({type: loadDataAction, data: response }))
  }
}

// Connected Component
const App = connect(
  mapStateToProps,
  mapDispatchToProps
)(Movies)

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('root')
)