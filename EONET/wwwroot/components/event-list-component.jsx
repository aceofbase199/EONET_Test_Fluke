import { DatePicker, Select, Table } from 'antd';
import Axios from 'axios';
import React from 'react';
import moment from 'moment';
import { Link } from 'react-router-dom';
import { openErrorNotification, openSuccessNotification } from '../helpers/notification-helper';

const { Option } = Select;
const defaultLabeledOption = { key: -1, label: 'All' };

export class EventListComponent extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            events: [],
            categoriesOptions: [],
            filters: {
                statusOptions: ['All', 'Open', 'Closed'], 
                selectedСategory: defaultLabeledOption,
                selectedStatus: 'All',
                selectedDate: moment.utc().add(-1, 'M'),
            },
            sorting: {
                sortField: 'Title',
                sortOrder: 'ascend',
            },
            pagination: {
                pageSize: 10,
                pageNumber: 1   
            },
            loading: true
        };
    }

    componentDidMount() {
        this.loadEvents();
        this.loadCategories();
    }

    getFilterModel = () => {
        const { filters, sorting } = this.state;

        return {
            status: filters && filters.selectedStatus || undefined,
            category: filters && filters.selectedСategory && filters.selectedСategory.key || defaultLabeledOption.key,
            date: filters && filters.selectedDate || moment.utc().add(-1, 'M'),
            sortField: sorting && sorting.sortField || 'Title',
            sortOrder: sorting && sorting.sortOrder || 'ascend'
        };
    }

    loadEvents = () => {
        const { pagination } = this.state;
        const url = `/events?pageSize=${pagination.pageSize}&pageNumber=${pagination.pageNumber}`;

        this.changeValueByKey('loading')(true);
        Axios.post(url, this.getFilterModel())
            .then(({ data }) => {
                if (data.succeeded && data.data) {
                    this.setState({
                        loading: false,
                        events: data.data,
                        pagination: {
                            total: data.totalRecords,
                        },
                      });
                    openSuccessNotification("Events loaded successfully");
                } else {
                    openErrorNotification("Failed to load events");
                }
            })
            .catch(error => {
                console.error(error);
                openErrorNotification("Failed to load events");
            })
            .finally(() => this.changeValueByKey('loading')(false));
    }

    loadCategories = () => {
        Axios.get(`/categories`)
            .then(({ data }) => {
                if (data) {
                    const mappedCategories = data.map(x => { return { key: x.id, label: x.title }});

                    if (!mappedCategories.find(x => x.key === defaultLabeledOption.key))
                        mappedCategories.unshift(defaultLabeledOption);

                    this.setState((prevState) => {
                        return {
                            ...prevState,
                            filters: {
                                ...prevState.filters,
                                selectedСategory: defaultLabeledOption
                            },
                            categoriesOptions: mappedCategories
                        }
                    });
                    openSuccessNotification("Categories loaded successfully");
                } else {
                    openErrorNotification("Failed to load categories");
                }
            })
            .catch(error => {
                console.error(error);
                openErrorNotification("Failed to load categories");
            });
    }

    getColumns = () => {
        return [
            {
                title: 'Id',
                dataIndex: 'id',
                key: 0,
                sorter: true,
                sortDirections: ['descend', 'ascend'],
            }, {
                key: 1,
                title: 'Title',
                dataIndex: 'title',
                render: (cell, record) => <Link to={`/events/${record.id}`}>{cell}</Link> ,
                sorter: true,
                sortDirections: ['descend', 'ascend'],
            }, {
                key: 2,
                title: 'Description',
                dataIndex: 'description',
            }, {
                key: 3,
                title: 'Link',
                dataIndex: 'link',
                render: text => <a href={text}>{text}</a>,
            }
        ]
    }

    changeValueByKey = key => value => {
        this.setState((prevState) => {
            return {
                ...prevState,
                [`${key}`]: value.target ? value.target.value : value
            }
        });
    }

    handleTableChange = (pagination, _filters, sorter) => {
        this.setState((prevState) => {
            return {
                ...prevState,
                sorting: {
                    ...prevState.sorting,
                    sortField: sorter.field,
                    sortOrder: sorter.order
                },
                pagination: {
                    ...prevState.pagination,
                    pageSize: pagination.pageSize,
                    pageNumber: pagination.pageSize
                }
            }}, () => this.loadEvents()
        );
    }

    getPagination = () => {
        const { events } = this.state;

        return {
            defaultPageSize: 10,
            showTotal: true,
            total: events.length
        };
    }

    handleFilterChange = key => value => {
        this.setState((prevState) => {
            return {
                ...prevState,
                filters: {
                    ...prevState.filters,
                    [key]: value
                }
            }}, () => this.loadEvents()
        );
    }

    disabledDate = (current) => {
        return current && current >= moment().endOf('day');
    } 

    render () {
        const { events, filters, pagination, loading, categoriesOptions  } = this.state;

        return (
            <>
                <div className="row mr-s-15">
                    <div className="col-md-3 col-sm-12">
                        <span className="pr-10">Category</span>
                        <Select className="w-100"
                            value={filters.selectedСategory}
                            labelInValue
                            onChange={this.handleFilterChange('selectedСategory')}>
                            {categoriesOptions && categoriesOptions.map(opt => {
                                return <Option key={opt.key} value={opt.key} title={opt.label}>{opt.label}</Option>
                            })}
                        </Select>
                    </div>
                    <div className="col-md-3 col-sm-12">
                        <span className="pr-10">Status</span>
                        <Select className="w-100"
                            value={filters.selectedStatus}
                            onChange={this.handleFilterChange('selectedStatus')}>
                            {filters.statusOptions.map((opt, index) => {
                                return <Option key={index} value={opt} title={opt}>{opt}</Option>
                            })}
                        </Select>
                    </div>
                    <div className="col-md-3 col-sm-12">
                        <span className="pr-10">Date</span>
                        <DatePicker className="w-100"
                            disabledDate={this.disabledDate}
                            value={filters.selectedDate} 
                            onChange={this.handleFilterChange('selectedDate')} />
                    </div>
                </div>
                <Table columns={this.getColumns()}
                    dataSource={events}
                    loading={loading}
                    rowKey={record => record.id}
                    pagination={pagination}
                    onChange={this.handleTableChange}
                    scroll={{ x: '100%' }}
                    className='m-s-10'
                    rowClassName={(record) => !record.isOpen ? 'closed-event' : ''}/>
            </>
        )
    }
}